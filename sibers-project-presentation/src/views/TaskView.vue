<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { Task } from '../types/task-type.ts';
import { taskApi } from '../api/task-api.ts';
import TaskList from '../components/task/TaskList.vue';
import CreateTaskForm from '../components/task/CreateTaskForm.vue';
import UpdateTaskForm from '../components/task/UpdateTaskForm.vue';
import { useAuthStore } from '../store/authStore.ts';

const tasks = ref<Task[]>([]);
const loading = ref<boolean>(true);
const error = ref<string | null>(null);
const isUpdating = ref<boolean>(false);
const selectedTask = ref<Task | null>(null);
const authStore = useAuthStore();

const fetchTasks = async () => {
  loading.value = true;
  try {
    const userRole = authStore.role;
    const userId = Number(authStore.id);
    if (userRole === 'Director') {
      tasks.value = await taskApi.getAllTasks();
    } else if (userRole === 'ProjectManager') {
      tasks.value = await taskApi.getTasksByAuthorId(userId);
    } else if (userRole === 'Employee') {
      tasks.value = await taskApi.getTasksByEmployeeId(userId);
    }
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
      <div
        v-if="
          authStore.role === 'Director' || authStore.role === 'ProjectManager'
        "
      >
        <CreateTaskForm @on-task-create="onTaskCreated" />
      </div>
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
