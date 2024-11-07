<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { userService } from '../../../services/user-service.ts';
import { projectService } from '../../../services/project-service.ts';
import type { User } from '../../../services/user-service.ts';

const route = useRoute();
const projectId = Number(route.params.projectId);

const assignedUsers = ref<User[]>([]);
const allUsers = ref<User[]>([]);
const selectedUserId = ref<number | null>(null);

onMounted(async () => {
  assignedUsers.value = await userService.getUsersByProjectId(projectId);
  allUsers.value = await userService.getAllUsers();
});

const addUserToProject = async () => {
  if (selectedUserId.value !== null) {
    await projectService.addEmployeeToProject({
      EmployeeId: selectedUserId.value,
      ProjectId: projectId,
    });
    assignedUsers.value = await userService.getUsersByProjectId(projectId);
  }
};

const removeUserFromProject = async (userId: number) => {
  await projectService.removeEmployeeFromProject({
    EmployeeId: userId,
    ProjectId: projectId,
  });
  assignedUsers.value = await userService.getUsersByProjectId(projectId);
};
</script>

<template>
  <div>
    <h2>Управление пользователями для проекта {{ projectId }}</h2>
    <select v-model="selectedUserId">
      <option v-for="user in allUsers" :key="user.id" :value="user.id">
        {{ user.firstName }}
      </option>
    </select>
    <button @click="addUserToProject">Добавить</button>

    <h3>Назначенные пользователи</h3>
    <ul>
      <li v-for="user in assignedUsers" :key="user.id">
        {{ user.firstName }}
        <button @click="removeUserFromProject(user.id)">Удалить</button>
      </li>
    </ul>
  </div>
</template>

<style scoped>

</style>