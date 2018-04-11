<template>
    <modal name="image-dialog"
           :adaptive="true"
           :scrollable="true"
           :reset="true"
           :min-width="200"
           :min-height="200"
           width="60%"
           height="auto"
           @before-open="beforeOpen">

        <div class="text-center">
            <h3><b>{{fileName}}</b></h3>
            <img v-bind:src="image" class="rounded">
        </div>
    </modal>
</template>

<script>
    export default {
        data() {
            return {
                fileName: '',
                image: ''
            }
        },
        methods: {
            beforeOpen(event) {
                this.fileName = event.params.fileName;
                this.image = event.params.image;
            }
        }
}
</script>
<style>
    .rounded {
        height: 100%;
        width: 100%;
        padding: 10px;
    }

    .text-center {
        height: auto;
        width: auto;
    }
</style>