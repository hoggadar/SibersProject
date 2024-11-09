<script setup lang="ts">
import { Project } from '../../types/project-type';

interface IProps {
  selectedProjectId: number | undefined;
  projects: Project[];
  loading: boolean;
  error: string | null;
}

const props = defineProps<IProps>();

const emit = defineEmits<{
  (e: 'delete', project: Project): void;
  (e: 'start-updating', project: Project): void;
  (e: 'cancel-updating'): void;
}>();

const startUpdating = (project: Project) => {
  emit('start-updating', project);
};

const cancelUpdating = () => {
  emit('cancel-updating');
};
</script>

<template>
  <div>
    <div v-if="props.loading" class="text-blue-500">Загрузка...</div>
    <div v-if="props.error" class="text-red-500">{{ props.error }}</div>

    <table
      v-if="!props.loading && props.projects.length"
      class="bg-white border border-gray-200 shadow-md rounded-lg overflow-hidden"
    >
      <thead class="bg-gray-200">
        <tr>
          <th class="py-2 px-4 text-center">ID</th>
          <th class="py-2 px-4 text-center">Название</th>
          <th class="py-2 px-4 text-center">Заказчик</th>
          <th class="py-2 px-4 text-center">Исполнитель</th>
          <th class="py-2 px-4 text-center">Директор</th>
          <th class="py-2 px-4 text-center">Дата начала</th>
          <th class="py-2 px-4 text-center">Дата окончания</th>
          <th class="py-2 px-4 text-center">Приоритет</th>
          <th class="py-2 px-4 text-center">Действия</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-gray-200">
        <tr
          v-for="project in props.projects"
          :key="project.id"
          class="hover:bg-gray-100"
        >
          <td class="py-2 px-4">{{ project.id }}</td>
          <td class="py-2 px-4">{{ project.title }}</td>
          <td class="py-2 px-4">{{ project.customerCompany }}</td>
          <td class="py-2 px-4">{{ project.performerCompany }}</td>
          <td class="py-2 px-4">{{ project.directorId }}</td>
          <td class="py-2 px-4">
            {{ new Date(project.startDate).toLocaleDateString() }}
          </td>
          <td class="py-2 px-4">
            {{ new Date(project.endDate).toLocaleDateString() }}
          </td>
          <td class="py-2 px-4">{{ project.priority }}</td>
          <td class="py-2 px-4 w-[420px] flex justify-center">
            <div v-if="selectedProjectId === project.id">
              <button
                @click="cancelUpdating()"
                class="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
              >
                Cancel
              </button>
            </div>
            <div v-else class="flex gap-2">
              <router-link
                :to="{ path: `/project/${project.id}` }"
                class="bg-green-600 hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
              >
                Подробнее
              </router-link>
              <button
                :disabled="!!selectedProjectId"
                @click="startUpdating(project)"
                class="bg-yellow-400 hover:bg-yellow-500 text-white font-bold py-2 px-4 rounded"
              >
                Редактировать
              </button>
              <button
                :disabled="!!selectedProjectId"
                @click="() => emit('delete', project)"
                class="bg-red-600 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
              >
                Удалить
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <div
      v-else-if="!props.loading && !props.projects.length"
      class="mt-4 text-gray-600"
    >
      Нет проектов для отображения.
    </div>
  </div>
</template>

<style scoped></style>
