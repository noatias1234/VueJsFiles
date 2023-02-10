<script>
import fs from "fs";
import pathModule from "path";
import { app } from "@electron/remote";
import { computed, ref } from "vue";

import FilesViewer from "./components/FilesViewer.vue";

const formatSize = (size) => {
  var i = Math.floor(Math.log(size) / Math.log(1024));
  return (
    (size / Math.pow(1024, i)).toFixed(2) * 1 +
    " " +
    ["B", "kB", "MB", "GB", "TB"][i]
  );
};

export default {
  components: { FilesViewer },
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
  },
  setup() {
    const path = ref(app.getAppPath());
    const files = computed(() => {
      const fileNames = fs.readdirSync(path.value);
      return fileNames

        .map((file) => {
          try {
            const stats = fs.statSync(pathModule.join(path.value, file));
            return {
              name: file,
              size: stats.isFile() ? formatSize(stats.size ?? 0) : null,
              directory: stats.isDirectory(),
            };
          } catch (err) {
            // console.error(err);
            return {
              name: file,
              size: null,
              directory: false,
            };
          }
        })
        .sort((a, b) => {
          if (a.directory === b.directory) {
            return a.name.localeCompare(b.name);
          }
          return a.directory ? -1 : 1;
        });
    });

    const back = () => {
      path.value = pathModule.dirname(path.value);
    };

    const open = (folder) => {
      path.value = pathModule.join(path.value, folder);
    };

    const searchString = ref("");
    const filteredFiles = computed(() => {
      return searchString.value
        ? files.value.filter((s) => s.name.startsWith(searchString.value))
        : files.value;
    });

    return { path, open, back, files, searchString, filteredFiles };
  },
};
</script>

<template>
  <div class="container mt-2">
    <h4>{{ path }}</h4>
    <div class="form-group mt-4 mb-2">
      <input
        v-model="searchString"
        class="form-control form-control-sm"
        placeholder="File search"
      />
    </div>
    <FilesViewer
      :files="filteredFiles"
      @back="back"
      @folderClick="open($event.name)"
    />
  </div>
  <div>
    <form>
      <div class="form-group mt-4 mb-2">
      <label for="fileInput">Select Files:</label>
      <input
        type="file"
        class="form-control-file"
        id="fileInput"
        ref="fileInput"
        @change="selectFiles"
        multiple
      />
    </div>
    </form>
    

    <table class="table">
      <thead>
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
  </div>
</template>
