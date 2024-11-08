<script setup lang="ts">
import { ProjectDto, UpdateProjectDto } from "../../types/project-type";
import {projectApi} from "../../api/project-api.ts";
import {ref, watch} from "vue";

const props = defineProps<{
  project: ProjectDto;
}>();

const updatedProject = ref<UpdateProjectDto>({
  title: '',
  customerCompany: '',
  performerCompany: '',
  directorId: 0,
  priority: 0,
});

const message = ref<string | null>(null);
const error = ref<string | null>(null);

watch(() => props.project, (newValue) => {
  updatedProject.value = {
    title: newValue.title,
    customerCompany: newValue.customerCompany,
    performerCompany: newValue.performerCompany,
    directorId: newValue.directorId,
    priority: newValue.priority,
  }
}, {immediate: true});

const submitForm = async () => {
  try {
    await projectApi.updateProject(props.project.id, updatedProject.value);
    console.log(updatedProject.value);
    message.value = 'Проект успешно обновлен!';
    error.value = null;
    resetForm();
  } catch (err) {
    error.value = 'Ошибка при обновлении проекта.';
    message.value = null;
  }
};

const resetForm = () => {
  updatedProject.value = {
    title: '',
    customerCompany: '',
    performerCompany: '',
    directorId: 0,
    priority: 1,
  };
};
</script>

<template>
  <div class="p-6">
    <h2 class="text-2xl font-bold mb-4">Добавить новый проект</h2>

    <form @submit.prevent="submitForm" class="grid grid-cols-2 gap-4 w-[600px]">
      <div class="flex flex-col">
        <div>
          <label for="title" class="block">Название проекта:</label>
          <input v-model="updatedProject.title" type="text" id="title" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>

        <div>
          <label for="customerCompany" class="block">Заказчик:</label>
          <input v-model="updatedProject.customerCompany" type="text" id="customerCompany" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>

        <div>
          <label for="performerCompany" class="block">Исполнитель:</label>
          <input v-model="updatedProject.performerCompany" type="text" id="performerCompany" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>
      </div>

      <div class="flex flex-col">
        <div>
          <label for="directorId" class="block">ID Директора:</label>
          <input v-model.number="updatedProject.directorId" type="number" id="directorId" required class="w-full border border-gray-300 rounded px-3 py-2"/>
        </div>

        <div>
          <label for="priority" class="block">Приоритет:</label>
          <select v-model.number="updatedProject.priority" id="priority" class="w-full border border-gray-300 rounded px-3 py-2">
            <option value="1">1 (Низкий)</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5 (Высокий)</option>
          </select>
        </div>
      </div>

      <div class="col-span-2">
        <button type="submit" class="bg-blue-500 text-white rounded px-4 py-2">Сохранить изменения</button>
      </div>
    </form>

    <div v-if="message" class="mt-4 text-green-500">{{ message }}</div>
    <div v-if="error" class="mt-4 text-red-500">{{ error }}</div>
  </div>
</template>

<style scoped>

</style>