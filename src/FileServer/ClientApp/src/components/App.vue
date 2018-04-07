<template>
    <div id="app">
        <file-list />
    </div>
</template>

<script>
    import FileList from './FileList.vue';

    export default {
        name: 'app',
        components: { FileList }
    }
</script>