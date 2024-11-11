<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { Task } from '../types/task-type.ts';
import { taskApi } from '../api/task-api.ts';
import TaskList from '../components/task/TaskList.vue';
import CreateTaskForm from '../components/task/CreateTaskForm.vue';
import UpdateTaskForm from '../components/task/UpdateTaskForm.vue';

const tasks = ref<Task[]>([]);
const loading = ref<boolean>(true);
const error = ref<string | null>(null);
const isUpdating = ref<boolean>(false);
const selectedTask = ref<Task | null>(null);

const fetchTasks = async () => {
  loading.value = true;
  try {
    tasks.value = await taskApi.getAllTasks();
  } catch (err) {
    error.value = 'Не удалось загрузить задачи.';
  } finally {
    loading.value = false;
  }
};

const handleDelete = async (taskToDelete: Task) => {
  try {
    await taskApi.deleteTask(taskToDelete.id);
    tasks.value = tasks.value.filter((task) => task.id !== taskToDelete.id);
  } catch (err) {
    error.value = 'Ошибка при удалении задачи.';
  }
};

const handleStartUpdating = (task: Task) => {
  isUpdating.value = true;
  selectedTask.value = task;
};

const handleCancelUpdating = () => {
  isUpdating.value = false;
  selectedTask.value = null;
};

const onTaskCreated = () => {
  fetchTasks();
};

const onTaskUpdated = () => {
  isUpdating.value = false;
  selectedTask.value = null;
  fetchTasks();
};

onMounted(fetchTasks);
</script>

<template>
  <div class="p-6">
    <div v-if="isUpdating && selectedTask">
      <UpdateTaskForm :task="selectedTask" @on-task-update="onTaskUpdated" />
    </div>
    <div v-else>
      <CreateTaskForm @on-task-create="onTaskCreated" />
    </div>
    <TaskList
      :tasks="tasks"
      :selected-task-id="selectedTask?.id"
      :loading="loading"
      :error="error"
      @delete="handleDelete"
      @startUpdating="handleStartUpdating"
      @cancelUpdating="handleCancelUpdating"
    />
  </div>
</template>

<style scoped></style>
