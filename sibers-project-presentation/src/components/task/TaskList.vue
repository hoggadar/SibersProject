<script setup lang="ts">
import { Task } from '../../types/task-type.ts';

interface IProps {
  selectedTaskId: number | undefined;
  tasks: Task[];
  loading: boolean;
  error: string | null;
}

const props = defineProps<IProps>();

const emit = defineEmits<{
  (e: 'delete', task: Task): void;
  (e: 'start-updating', task: Task): void;
  (e: 'cancel-updating'): void;
}>();

const startUpdating = (task: Task) => {
  emit('start-updating', task);
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
      v-if="!props.loading && props.tasks.length"
      class="bg-white border border-gray-200 shadow-md rounded-lg overflow-hidden"
    >
      <thead class="bg-gray-200">
        <tr>
          <th class="py-2 px-4 text-center">ID</th>
          <th class="py-2 px-4 text-center">Название</th>
          <th class="py-2 px-4 text-center">Автор</th>
          <th class="py-2 px-4 text-center">Исполнитель</th>
          <th class="py-2 px-4 text-center">Проект</th>
          <th class="py-2 px-4 text-center">Приоритет</th>
          <th class="py-2 px-4 text-center">Статус</th>
          <th class="py-2 px-4 text-center">Действия</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-gray-200">
        <tr
          v-for="task in props.tasks"
          :key="task.id"
          class="hover:bg-gray-100"
        >
          <td class="py-2 px-4">{{ task.id }}</td>
          <td class="py-2 px-4">{{ task.title }}</td>
          <td class="py-2 px-4">{{ task.authorId }}</td>
          <td class="py-2 px-4">{{ task.performerId }}</td>
          <td class="py-2 px-4">{{ task.projectId }}</td>
          <td class="py-2 px-4">{{ task.priority }}</td>
          <td class="py-2 px-4">{{ task.status }}</td>
          <td class="py-2 px-4 w-[274px] flex justify-center">
            <div v-if="selectedTaskId === task.id">
              <button
                @click="cancelUpdating()"
                class="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
              >
                Отменить
              </button>
            </div>
            <div v-else class="flex gap-2">
              <button
                :disabled="!!selectedTaskId"
                @click="startUpdating(task)"
                class="bg-yellow-400 hover:bg-yellow-500 text-white font-bold py-2 px-4 rounded"
              >
                Редактировать
              </button>
              <button
                :disabled="!!selectedTaskId"
                @click="() => emit('delete', task)"
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
      v-else-if="!props.loading && !props.tasks.length"
      class="mt-4 text-gray-600"
    >
      Нет задач для отображения.
    </div>
  </div>
</template>

<style scoped></style>
