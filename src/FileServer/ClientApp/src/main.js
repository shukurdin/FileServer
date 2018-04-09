import Vue from 'vue';
import App from './components/App.vue';
import VModal from 'vue-js-modal'

Vue.use(VModal)

// ReSharper disable once ConstructorCallNotUsed
new Vue({
    el: '#app',
    template: '<App/>',
    components: { App }
})