import { YTGUtils } from "./utils.js";
import { argv } from "process";

async function buildConfig(version) {
  return await YTGUtils.setBuildVersion("package.json", version);
}

buildConfig(argv[2]).then((result) => {
  console.log(result);
});
