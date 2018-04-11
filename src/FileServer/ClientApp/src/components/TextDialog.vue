<template>
    <modal name="text-dialog"
           :adaptive="true"
           :scrollable="true"
           :reset="true"
           :min-width="200"
           :min-height="200"
           width="60%"
           height="auto"
           @before-open="beforeOpen">
        <div>
            <h3 class="text-center"><b>{{fileName}}</b></h3>
            <div class="content"><p>{{text}}</p></div>
        </div>
    </modal>
</template>

<script>
    export default {
        data() {
            return {
                fileName: '',
                text: ''
            }
        },
        methods: {
            beforeOpen(event) {
                this.fileName = event.params.fileName;
                this.text = event.params.text;
            }
        }
    }
</script>

<style>
    .content {
        padding: 10px;
        font-style: 13px;
    }
</style>
