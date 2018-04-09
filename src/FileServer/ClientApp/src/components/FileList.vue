<template>
    <div id="file-list">
        <div class="page-header">
            <h1>Список файлов</h1>
        </div>
        <div class="panel">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <td>Название</td>
                        <td>Дата изменения</td>
                        <td>Размер</td>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="file in files">
                        <td>{{file.name}}</td>
                        <td>{{file.lastModifiedDate}}</td>
                        <td>{{file.lenght}}</td>
                        <td>
                            <button class="btn btn-sm btn-primary"
                                    v-on:click="open(file.name)">
                                Открыть
                            </button>
                        </td>
                        <td>
                            <button class="btn btn-sm btn-danger"
                                    v-on:click="remove(file.name)">
                                Удалить
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <modal name="file-view">
            <div>
                <h3>Hello</h3>
                <img v-bind:src="image" v-show="image" />
                <p v-show="text">{{text}}</p>
            </div>
        </modal>
    </div>
</template>

<script>
    import axios from 'axios';
    import Dialog from './Dialog.vue';

    const API_PREFIX = '/api/files/';

    export default {
        name: 'file-list',
        data() {
            return {
                files: [],
                image: '',
                text: ''
            }
        },
        created() {
            this.getFiles();
        },
        methods: {
            open(fileName) {

                var isImage = false;
                if (/\.(jpe?g|png|gif)$/i.test(fileName)) {
                    this.text = '';
                    isImage = true;
                }
                else if (/\.txt$/i.test(fileName)) {
                    this.image = '';
                } else {
                    return; // other files
                }

                axios.get(API_PREFIX + fileName, {
                    responseType: 'blob'
                })
                    .then(response => {
                        var reader = new FileReader();
                        reader.addEventListener("load", function () {
                            if (isImage) {
                                this.image = reader.result;
                            }
                            else {
                                this.text = reader.result;
                            }
                        }.bind(this), false);

                        if (response.data != null) {
                            if (isImage)
                                reader.readAsDataURL(response.data);
                            else
                                reader.readAsText(response.data, "UTF-8");
                        }

                    })
                    .catch(error => {
                        console.log(error);
                    });

                this.$modal.show('file-view');
            },
            getFiles() {
                axios.get(API_PREFIX)
                    .then(response => {
                        this.files = response.data;
                        for (var i = 0; i < this.files.length; i++) {
                            files[i].canOpen = true;
                        }
                    })
                    .catch(error => {
                        console.log(error);
                    });
            },
            remove(name) {
                axios.delete(API_PREFIX + name)
                    .then(response => {
                        this.getFiles();
                    })
                    .catch(error => {
                        console.log(error);
                    });
            }
        }
    }
</script>