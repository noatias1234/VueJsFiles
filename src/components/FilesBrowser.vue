<script>
export default {
  data() {
    return {
      selectedFiles: [],
    };
  },
  methods: {
    selectFiles() {
      this.selectedFiles = this.$refs.fileInput.files;
    },

    formatSize(size) {
      var i = Math.floor(Math.log(size) / Math.log(1024));
      return (
        (size / Math.pow(1024, i)).toFixed(2) * 1 +
        " " +
        ["B", "kB", "MB", "GB", "TB"][i]
      );
    },
  }
};
</script>

<template>
  <div class="container mt-2">
    <div class="mb-3">
      <div class="input-group mb-3">
        <input type="file" class="form-control shadow-none" id="fileInput" ref="fileInput" @change="selectFiles"
          multiple />
        <button :disabled="selectedFiles.length === 0" class="btn btn-success shadow-none" type="button"
          id="button-addon2">Upload
        </button>
      </div>
      <div v-if="selectedFiles.length > 0">
        <table class="table">
          <thead class="table-light">
            <tr>
              <th>File Name</th>
              <th>Size</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(file, index) in selectedFiles" :key="index">
              <td>{{ file.name }}</td>
              <td>{{ formatSize(file.size) }}</td>
            </tr>
          </tbody>
        </table>
        <div class="progress">
          <div class="progress-bar" role="progressbar" style="width: 25%;" aria-valuenow="25" aria-valuemin="0"
            aria-valuemax="100">25%</div>
        </div>
        <br>
      </div>
    </div>
  </div>
</template>