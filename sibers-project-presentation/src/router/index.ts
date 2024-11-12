import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import UserView from '../views/UserView.vue';
import ProjectView from '../views/ProjectView.vue';
import LoginView from '../views/LoginView.vue';
import SignupView from '../views/SignupView.vue';
import ProjectDetailView from '../views/ProjectDetailView.vue';
import { useAuthStore } from '../store/authStore.ts';
import TaskView from '../views/TaskView.vue';
import AccessDenied from '../components/auth/AccessDenied.vue';

interface RouteMeta {
  requiresAuth?: boolean;
  allowedRoles?: string[];
}

type ExtendedRouteRecordRaw = RouteRecordRaw & {
  meta?: RouteMeta;
};

const routes: Array<ExtendedRouteRecordRaw> = [
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
    meta: { requiresAuth: true, allowedRoles: ['Director'] },
  },
  {
    path: '/project',
    name: 'project',
    component: ProjectView,
    meta: {
      requiresAuth: true,
      allowedRoles: ['Director', 'ProjectManager', 'Employee'],
    },
  },
  {
    path: '/project/:projectId',
    name: 'project-users',
    component: ProjectDetailView,
    props: true,
    meta: {
      requiresAuth: true,
      allowedRoles: ['Director', 'ProjectManager', 'Employee'],
    },
  },
  {
    path: '/task',
    name: 'task',
    component: TaskView,
    meta: {
      requiresAuth: true,
      allowedRoles: ['Director', 'ProjectManager', 'Employee'],
    },
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
  {
    path: '/access-denied',
    name: 'AccessDenied',
    component: AccessDenied,
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
  } else if (to.meta.allowedRoles && Array.isArray(to.meta.allowedRoles)) {
    if (to.meta.allowedRoles.includes(authStore.role)) {
      next();
    } else {
      next({ name: 'AccessDenied' });
    }
  } else {
    next();
  }
});

export default router;
