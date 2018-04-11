<template>
    <div id="app">
        <div>
            <file-list ref="list"/>
        </div>
    </div>
</template>

<script>

    import FileList from './Filelist.vue'
    
    export default {
        name: 'app',
        components: { FileList }
    }
</script>