<script setup lang="ts">
import {onMounted, ref} from "vue";
import {Project, projectService} from "../services/project-service.ts";
import ProjectList from "../components/project/ProjectList.vue";
import UpdateProjectForm from "../components/project/UpdateProjectForm.vue";
import CreateProjectForm from "../components/project/CreateProjectForm.vue";

const projects = ref<Project[]>([]);
const loading = ref<boolean>(true);
const error = ref<string | null>(null);
const isUpdating = ref<boolean>(false);
const selectedProject = ref<Project | null>(null);

const fetchProjects = async () => {
  loading.value = true;
  try {
    projects.value = await projectService.getAllProjects();
  } catch (err) {
    error.value = 'Не удалось загрузить пользователей.';
  } finally {
    loading.value = false;
  }
};

const handleDeleteProject = async (projectToDelete: Project) => {
  try {
    await projectService.deleteProject(projectToDelete.id);
    projects.value = projects.value.filter(project => project.id !== projectToDelete.id);
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

onMounted(fetchProjects);
</script>

<template>
  <div class="p-6">
    <div v-if="isUpdating && selectedProject">
      <UpdateProjectForm
          :project="selectedProject"
      />
    </div>
    <div v-else>
      <CreateProjectForm />
    </div>
    <ProjectList
        :projects="projects"
        :loading="loading"
        :error="error"
        :on-delete="handleDeleteProject"
        :on-start-updating="handleStartUpdating"
        :on-cancel-updating="handleCancelUpdating"
    />
  </div>
</template>

<style scoped>

</style>