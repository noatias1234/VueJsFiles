import axios from "axios";
var progress = 0;
var count = 0;
export async function uploadFile(file, filesCounter) {
  const data = { path: file.path };
  console.log(data);
  var timerName = `Time`;
  console.time(timerName);

  var response = await axios.post("https://localhost:7202/Files", data);
  console.timeEnd(timerName);

  console.log(response);
  count += 1;
  console.log(count);
  progress = Math.round((count / filesCounter) * 100);
  console.log(progress);

  return progress;
}
