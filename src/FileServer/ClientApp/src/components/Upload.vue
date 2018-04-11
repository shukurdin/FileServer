<template>
    <div class="container">
        <div class="large-12 medium-12 small-12 cell">
            <label class="btn btn-default btn-file">
                Выберите файл
                <input type="file" 
                       id="file" 
                       ref="fileupload" 
                       style="display: none;" 
                       v-on:change="handleFileUpload()" />
            </label>
            <label>{{file.name}}</label>
            <br>
            <progress class="progress-bar-info" max="100" :value.prop="uploadPercentage"></progress>
            <br>
            <button v-show="file" v-on:click="submitFile()">Отправить</button>
        </div>
    </div>
</template>

<script>

    import FileService from '../services/FileService';

    export default {
        data() {
            return {
                file: '',
                uploadPercentage: 0
            }
        },
        methods: {
            handleFileUpload() {
                this.file = this.$refs.fileupload.files[0];
            },
            submitFile() {

                var formData = new FormData();

                formData.append('file', this.file);

                FileService.uploadFile(formData, process => {
                    this.uploadPercentage = process;
                })
                    .then(files => {
                        console.log('success!');
                        this.uploadPercentage = 0;
                        this.clear();
                        this.$emit('onSuccess', files);
                    })
                    .catch(function () {
                        console.log('faled!');
                    });
            },
            clear() {
                const input = this.$refs.fileupload;
                input.type = 'text';
                input.type = 'file';
                console.log('clear');
            }
        }
    }
</script>

<style>
    .btn-file {
        position: relative;
        overflow: hidden;
    }

    .btn-file input[type=file] {
        position: absolute;
        top: 0;
        right: 0;
        min-width: 100%;
        min-height: 100%;
        font-size: 100px;
        text-align: right;
        filter: alpha(opacity=0);
        opacity: 0;
        outline: none;
        background: white;
        cursor: inherit;
        display: block;
    }
    
</style>