<template>
    <div id="file-list">
        <div class="page-header">
            <div class="container">
                <div class="row">
                    <div class="col-md-6 float-left">
                        <h1>Список файлов</h1>
                    </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3">
                        <div class="float-right">
                            <upload v-on:onSuccess="onUploaded" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <td>Название</td>
                        <td>Дата изменения</td>
                        <td>Размер (Кбайт)</td>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="file in files">
                        <td>{{file.name}}</td>
                        <td>{{file.lastModifiedDate | formatDate}}</td>
                        <td>{{file.length / 1000}}</td>
                        <td>
                            <button class="btn btn-sm btn-primary"
                                    v-if="canOpen(file.name)"
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
        <text-dialog />
        <image-dialog />
        <modals-container />
    </div>
</template>

<script>
    import axios from 'axios';
    import ImageDialog from './ImageDialog.vue';
    import TextDialog from './TextDialog.vue';
    import fileService from '../services/FileService';
    import Utils from '../utilites/Utils';
    import Upload from './Upload.vue'

    const API_PREFIX = '/api/files/';

    export default {
        name: 'file-list',
        components: { TextDialog, ImageDialog, Upload},
        data() {
            return {
                files: []
            }
        },
        created() {
            this.getFiles();
        },

        methods: {
            open(fileName) {
                fileService.getFile(fileName)
                    .then(data => {
                        if (Utils.isImage(fileName)) {
                            this.$modal.show('image-dialog', {
                                fileName: fileName,
                                image: data
                            });
                        }
                        else if (Utils.isText(fileName)) {
                            this.$modal.show('text-dialog',
                                {
                                    fileName: fileName,
                                    text: data
                                });
                        }
                    });
            },

            getFiles() {
                fileService.getFiles()
                    .then(data => this.files = data)
                    .catch(error => console.log(error));
            },

            remove(name) {

                fileService.removeFile(name)
                    .then(response => {
                        this.getFiles();
                    })
                    .catch(error => {
                        console.log(error);
                    });
            },

            onUploaded(files) {
                console.log('file name:' + files[0].name);
                this.file = files[0];
                this.getFiles();
            },

            canOpen(fileName) {
                return Utils.isImage(fileName) || Utils.isText(fileName);
            }
        }
    }
</script>