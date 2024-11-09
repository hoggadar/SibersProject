import { createApp } from 'vue';
import './style.css';
import App from './App.vue';
import router from './router';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import { createPinia } from 'pinia';

const vuetify = createVuetify({
  components,
  directives,
});

const app = createApp(App);
const pinia = createPinia();
app.use(router);
app.use(vuetify);
app.use(pinia);
app.mount('#app');
