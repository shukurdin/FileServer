import Vue from 'vue';
import App from './components/App.vue';
import VModal from 'vue-js-modal';
import moment from 'moment';

Vue.filter('formatDate',
    function (value) {
        if (value) {
            return moment(String(value)).format('MM/DD/YYYY hh:mm');
        }
    });

Vue.use(VModal,
    {
        dynamic: true,
        dialog: true
    });

// ReSharper disable once ConstructorCallNotUsed
new Vue({
    el: '#app',
    template: '<App/>',
    components: { App }
})