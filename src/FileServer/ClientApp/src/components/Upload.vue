<template>
    <div class="container">
        <div class="large-12 medium-12 small-12 cell">
            <label>
                File
                <input type="file" id="file" ref="file" v-on:change="handleFileUpload()" />
            </label>
            <br>
            <progress max="100" :value.prop="uploadPercentage"></progress>
            <br>
            <button v-show="file" v-on:click="submitFile()">Отправить</button>
        </div>
    </div>
</template>

<script>

    import axios from 'axios';

    export default {
        data() {
            return {
                file: '',
                uploadPercentage: 0
            }
        },
        methods: {
            handleFileUpload() {
                this.file = this.$refs.file.files[0];
            },
            submitFile() {

                var formData = new FormData();

                formData.append('file', this.file);

                axios.post('/api/files',
                    formData,
                    {
                        headers: {
                            'Content-Type': 'multipart/form-data'
                        },
                        onUploadProgress: function (progressEvent) {
                            this.uploadPercentage = parseInt(Math
                                .round(progressEvent.loaded * 100 / progressEvent.total));
                        }.bind(this)
                    }
                ).then(response => {
                    console.log('SUCCESS!!');
                    this.file = '';
                    this.$emit('onSuccess', response.data);
                })
                    .catch(function () {
                        console.log('FAILURE!!');
                });
            }
        }
    }
</script>