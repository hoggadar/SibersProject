<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { User } from '../../types/user-type.ts';
import { CreateProjectDto } from '../../types/project-type';
import { userApi } from '../../api/user-api.ts';
import { projectApi } from '../../api/project-api.ts';
import { useDropzone } from 'vue3-dropzone';

const emit = defineEmits(['on-project-create']);

const newProject = ref<CreateProjectDto>({
  title: '',
  customerCompany: '',
  performerCompany: '',
  directorId: 0,
  priority: 0,
});
const users = ref<User[]>([]);
const files = ref<File[]>([]);
const message = ref<string | null>(null);
const error = ref<string | null>(null);

const fetchUsers = async () => {
  try {
    users.value = await userApi.getAllUsers();
  } catch (err) {
    console.error(err);
    error.value = 'Ошибка при загрузке пользователей.';
  }
};

const submitForm = async () => {
  try {
    const projectResponse = await projectApi.createProject(newProject.value);
    emit('on-project-create');
    message.value = 'Проект успешно добавлен!';
    if (files.value.length > 0) {
      await projectApi.uploadFiles(projectResponse.id, files.value);
      message.value = 'Файлы успешно загружены!';
    }
    error.value = null;
    resetForm();
  } catch (err) {
    error.value = 'Ошибка при добавлении проекта.';
    message.value = null;
  }
};

const resetForm = () => {
  newProject.value = {
    title: '',
    customerCompany: '',
    performerCompany: '',
    directorId: 0,
    priority: 1,
  };
  files.value = [];
};

const { getRootProps, getInputProps, isDragActive, open } = useDropzone({
  onDrop: (acceptedFiles) => {
    files.value.push(...acceptedFiles);
  },
});

onMounted(fetchUsers);
</script>

<template>
  <div class="p-6">
    <h2 class="text-2xl font-bold mb-4">Добавить новый проект</h2>

    <form @submit.prevent="submitForm" class="grid grid-cols-2 gap-4 w-[600px]">
      <div class="flex flex-col">
        <div>
          <label for="title" class="block">Название проекта:</label>
          <input
            v-model="newProject.title"
            type="text"
            id="title"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>

        <div>
          <label for="customerCompany" class="block">Заказчик:</label>
          <input
            v-model="newProject.customerCompany"
            type="text"
            id="customerCompany"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>

        <div>
          <label for="performerCompany" class="block">Исполнитель:</label>
          <input
            v-model="newProject.performerCompany"
            type="text"
            id="performerCompany"
            required
            class="w-full border border-gray-300 rounded px-3 py-2"
          />
        </div>
      </div>

      <div class="flex flex-col">
        <div>
          <label for="directorId" class="block">Директор:</label>
          <v-autocomplete
            v-model="newProject.directorId"
            label="Выберите директора"
            required
            :items="users"
            :item-title="'email'"
            :item-value="'id'"
          />
        </div>

        <div>
          <label for="priority" class="block">Приоритет:</label>
          <select
            v-model.number="newProject.priority"
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
      </div>

      <div class="flex flex-col col-span-2">
        <label class="block">Загрузить PDF файлы:</label>
        <div
          v-bind="getRootProps()"
          class="border border-dashed border-gray-400 rounded p-4"
        >
          <input v-bind="getInputProps()" />
          <p v-if="isDragActive">Drop the files here ...</p>
          <p v-else>Drag 'n' drop some files here, or click to select files</p>
        </div>
        <button @click.prevent="open" class="mt-2 text-blue-500">
          Выбрать файлы
        </button>
        <div class="col-span-2 mt-4">
          <h3 class="font-semibold">Загруженные файлы:</h3>
          <ul>
            <li
              v-for="(file, index) in files"
              :key="index"
              class="text-gray-700"
            >
              {{ file.name }}
            </li>
          </ul>
        </div>
      </div>

      <div class="col-span-2">
        <button type="submit" class="bg-blue-500 text-white rounded px-4 py-2">
          Добавить проект
        </button>
      </div>
    </form>

    <div v-if="message" class="mt-4 text-green-500">{{ message }}</div>
    <div v-if="error" class="mt-4 text-red-500">{{ error }}</div>
  </div>
</template>

<style></style>
