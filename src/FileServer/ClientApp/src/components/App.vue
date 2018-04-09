<template>
    <div id="app">
        <div>
            <upload v-on:onSuccess="onUploaded"/>
            <label>{{file.name}}</label>
        </div>
        <div>
            <file-list ref="list"/>
        </div>
    </div>
</template>

<script>

    import FileList from './Filelist.vue'
    import Upload from './Upload.vue'

    export default {
        name: 'app',
        data() {
            return {
                file: ''
            }
        },
        components: { FileList, Upload },
        methods: {
            onUploaded(files) {
                console.log('file name:' + files[0].name);
                this.file = files[0];
                this.$refs.list.getFiles();
            }
        }
    }
</script>