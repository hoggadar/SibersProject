<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { projectApi } from '../api/project-api.ts';
import { Project } from '../types/project-type';
import ProjectList from '../components/project/ProjectList.vue';
import UpdateProjectForm from '../components/project/UpdateProjectForm.vue';
import CreateProjectForm from '../components/project/CreateProjectForm.vue';

const projects = ref<Project[]>([]);
const loading = ref<boolean>(true);
const error = ref<string | null>(null);
const isUpdating = ref<boolean>(false);
const selectedProject = ref<Project | null>(null);

const fetchProjects = async () => {
  loading.value = true;
  try {
    projects.value = await projectApi.getAllProjects();
  } catch (err) {
    error.value = 'Не удалось загрузить пользователей.';
  } finally {
    loading.value = false;
  }
};

const handleDelete = async (projectToDelete: Project) => {
  try {
    await projectApi.deleteProject(projectToDelete.id);
    projects.value = projects.value.filter(
      (project) => project.id !== projectToDelete.id
    );
  } catch (err) {
    error.value = 'Ошибка при удалении пользователя.';
  }
};

const handleStartUpdating = (project: Project) => {
  isUpdating.value = true;
  selectedProject.value = project;
};

const handleCancelUpdating = () => {
  isUpdating.value = false;
  selectedProject.value = null;
};

const onProjectCreated = () => {
  fetchProjects();
};

const onProjectUpdated = () => {
  isUpdating.value = false;
  selectedProject.value = null;
  fetchProjects();
};

onMounted(fetchProjects);
</script>

<template>
  <div class="p-6">
    <div v-if="isUpdating && selectedProject">
      <UpdateProjectForm
        :project="selectedProject"
        @on-project-update="onProjectUpdated"
      />
    </div>
    <div v-else>
      <CreateProjectForm @on-project-create="onProjectCreated" />
    </div>
    <ProjectList
      :projects="projects"
      :selected-project-id="selectedProject?.id"
      :loading="loading"
      :error="error"
      @delete="handleDelete"
      @startUpdating="handleStartUpdating"
      @cancelUpdating="handleCancelUpdating"
    />
  </div>
</template>

<style scoped></style>
