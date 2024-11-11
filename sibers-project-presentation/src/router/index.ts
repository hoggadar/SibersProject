import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import UserView from '../views/UserView.vue';
import ProjectView from '../views/ProjectView.vue';
import LoginView from '../views/LoginView.vue';
import SignupView from '../views/SignupView.vue';
import ProjectDetailView from '../views/ProjectDetailView.vue';
import { useAuthStore } from '../store/authStore.ts';
import TaskView from '../views/TaskView.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
    meta: { requiresAuth: true },
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
    path: '/task',
    name: 'task',
    component: TaskView,
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
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, _, next) => {
  const authStore = useAuthStore();
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next({ name: 'login' });
  } else {
    next();
  }
});

export default router;
