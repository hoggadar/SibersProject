<script setup lang="ts">
import { taskApi } from '../../api/task-api.ts';
import { onMounted, ref } from 'vue';
import { StatusEnum, TaskDto } from '../../types/task-type.ts';
import { Project } from '../../types/project-type.ts';
import { projectApi } from '../../api/project-api.ts';
import { User } from '../../types/user-type.ts';
import { userApi } from '../../api/user-api.ts';

const emit = defineEmits(['on-task-create']);

const newTask = ref<TaskDto>({
  title: '',
  authorId: 0,
  performerId: 0,
  projectId: 0,
  priority: 1,
  status: StatusEnum.Todo,
});
const projects = ref<Project[]>([]);
const users = ref<User[]>([]);
const message = ref<string | null>(null);
const error = ref<string | null>(null);

const fetchProjects = async () => {
  try {
    projects.value = await projectApi.getAllProjects();
  } catch (err) {
    error.value = 'Ошибка при загрузке проектов.';
  }
};

const fetchUsers = async () => {
  try {
    users.value = await userApi.getAllUsers();
  } catch (err) {
    error.value = 'Ошибка при загрузке проектов.';
  }
};

const submitForm = async () => {
  try {
    await taskApi.createTask(newTask.value);
    emit('on-task-create');
    message.value = 'Задача успешно добавлена!';
    error.value = null;
    resetForm();
  } catch (err) {
    error.value = 'Ошибка при добавлении задачи.';
    message.value = null;
  }
};

const resetForm = () => {
  newTask.value = {
    title: '',
    authorId: 0,
    performerId: 0,
    projectId: 0,
    priority: 1,
    status: StatusEnum.Todo,
  };
};

onMounted(() => {
  fetchProjects();
  fetchUsers();
});
</script>

<template>
  <div class="p-6">
    <h2 class="text-2xl font-bold mb-4">Добавить новую задачу</h2>

    <form @submit.prevent="submitForm" class="grid grid-cols-2 gap-4 w-[600px]">
      <div class="flex flex-col">
        <div>
          <label for="title" class="block">Название:</label>
          <input
            v-model="newTask.title"
            type="text"
            id="title"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>

        <div>
          <label for="authorId" class="block">ID автора:</label>
          <v-autocomplete
            v-model="newTask.authorId"
            label="Выберите автора"
            required
            :items="users"
            :item-title="'email'"
            :item-value="'id'"
          />
        </div>

        <div>
          <label for="performerId" class="block">ID исполнителя:</label>
          <v-autocomplete
            v-model="newTask.performerId"
            label="Выберите автора"
            required
            :items="users"
            :item-title="'email'"
            :item-value="'id'"
          />
        </div>
      </div>

      <div class="flex flex-col">
        <div>
          <label for="projectId" class="block">ID проекта:</label>
          <v-autocomplete
            v-model="newTask.projectId"
            label="Выберите директора"
            required
            :items="projects"
            :item-title="'title'"
            :item-value="'id'"
          />
        </div>

        <div>
          <label for="priority" class="block">Приоритет:</label>
          <select
            v-model.number="newTask.priority"
            id="priority"
            class="w-full border border-gray-300 rounded px-3 py-2"
          >
            <option value="1">1 (Низкий)</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5 (Высокий)</option>
          </select>
        </div>

        <div>
          <label for="status" class="block">Статус:</label>
          <select
            v-model="newTask.status"
            id="status"
            class="w-full border border-gray-300 rounded px-3 py-2"
          >
            <option :value="StatusEnum.Todo">ToDo</option>
            <option :value="StatusEnum.InProgress">InProgress</option>
            <option :value="StatusEnum.Done">Done</option>
          </select>
        </div>
      </div>

      <div class="col-span-2">
        <button type="submit" class="bg-blue-500 text-white rounded px-4 py-2">
          Добавить задачу
        </button>
      </div>
    </form>

    <div v-if="message" class="mt-4 text-green-500">{{ message }}</div>
    <div v-if="error" class="mt-4 text-red-500">{{ error }}</div>
  </div>
</template>

<style scoped></style>
