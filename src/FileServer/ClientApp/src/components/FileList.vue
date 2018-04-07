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
                            <button class="btn btn-sm btn-primary">Открыть</button>
                            <button class="btn btn-sm btn-danger" v-on:click="remove(file.name)">Удалить</button>
                        </td>
                    </tr>
                </tbody>

            </table>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    const API_PREFIX = '/api/files/';
    
    export default {
        name: 'file-list',
        data: () => ({
            files: []
        }),
        created() {
            this.getFiles();
        },
        methods: {
            getFiles: function () {
                axios.get(API_PREFIX)
                    .then(response => {
                        this.files = response.data;
                    })
                    .catch(error => {
                        console.log(error);
                    });
            },
            remove: function (name) {
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