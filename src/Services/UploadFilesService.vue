<script>
import axios from 'axios';


export default {
  data() {
    return {
      progress: 0
    };
  },
  methods: {
     uploadFile(filesPath) {
      try {
        const response =  axios.post('https://jsonplaceholder.typicode.com/posts', filesPath, {
          onUploadProgress: (progressEvent) => {
            this.progress = Math.round((progressEvent.loaded * 100) / progressEvent.total);
          }
        });
        console.log(response.data);
      } catch (error) {
        console.error(error);
      }
      return this.progress;
    },

  }
};
</script>

<style>
.progress {
  height: 20px;
  margin-top: 20px;
}

.progress-bar {
  background-color: #4caf50;
}
</style>
