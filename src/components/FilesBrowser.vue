<script>
import UploadFilesService from '@/Services/UploadFilesService.vue';
export default {
  components:{
    UploadFilesService,
  },
  data() {
    return {
      formValues:{
        selectedFiles: [],
      },
      progress: 0
    };
  },
  methods: {
    selectFiles() {
      this.formValues.selectedFiles = this.$refs.fileInput.files;
    },

    formatSize(size) {
      var i = Math.floor(Math.log(size) / Math.log(1024));
      return (
        (size / Math.pow(1024, i)).toFixed(2) * 1 +
        " " +
        ["B", "kB", "MB", "GB", "TB"][i]
      );
    },
  submitForm(event){
    event.preventDefault();
    console.log('Form values:', this.formValues);
    console.log('Selected files', this.formValues.selectedFiles)
 },
  postFiles(){
    this.progress=this.$refs.uploadFilesService.uploadFile(this.formValues.selectedFiles);
  },
  
  }
};
</script>

<template>
  <UploadFilesService ref="uploadFilesService"></UploadFilesService>
  <form @submit="submitForm">
  <div class="container mt-2">
    <div class="mb-3">
      <div class="input-group mb-3">
        <input type="file" @change="postFiles" class="form-control shadow-none" id="fileInput" ref="fileInput" multiple/>
          <button id="button" @click="postFiles">Upload</button>
      </div>
      <div v-if="formValues.selectedFiles.length > 0">
        <table class="table">
          <thead class="table-light">
            <tr>
              <th>File Name</th>
              <th>Size</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(file, index) in formValues.selectedFiles" :key="index">
              <td>{{ file.name }}</td>
              <td>{{ formatSize(file.size) }}</td>
            </tr>
          </tbody>
        </table>
        <br>
      </div>
      <div class="progress">
          <div class="progress-bar" role="progressbar" :style="{width: progress + '%'}" :aria-valuenow="progress" aria-valuemin="0" aria-valuemax="100" >{{ progress }}%</div>
        </div>
    </div>
  </div>
  </form>
</template>


<style>
.progress {
  height: 20px;
  margin-top: 20px;
}

.progress-bar {
  background-color: #4caf50;
}
</style>
