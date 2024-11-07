import {createRouter, createWebHistory, RouteRecordRaw} from "vue-router";
import HomeView from "../views/HomeView.vue";
import UserView from "../views/UserView.vue";
import ProjectView from "../views/ProjectView.vue";
import LoginView from "../views/LoginView.vue";
import SignupView from "../views/SignupView.vue";
import ProjectDetailView from "../views/ProjectDetailView.vue";

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'home',
        component: HomeView,
    },
    {
        path: '/user',
        name: 'user',
        component: UserView,
    },
    {
        path: '/project',
        name: 'project',
        component: ProjectView,
    },
    {
        path: '/project/:projectId',
        name: 'project-users',
        component: ProjectDetailView,
        props: true,
    },
    {
        path: '/login',
        name: 'login',
        component: LoginView,
    },
    {
        path: '/signup',
        name: 'signup',
        component: SignupView,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;