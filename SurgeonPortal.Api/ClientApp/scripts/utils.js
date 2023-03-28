import {
  existsSync,
  readFileSync,
  writeFileSync,
  rmdirSync,
  copyFile,
  constants,
} from "fs";
import { execSync, exec } from "child_process";

export class YTGUtils {
  static async copyFileFromTo(
    src,
    dest,
    mode = constants.COPYFILE_FICLONE,
    callback
  ) {
    console.log(`copyFile From ${src} to ${dest}`);
    try {
      await copyFile(src, dest, mode, () => {
        console.log("file is copied");
        YTGUtils.executeCommand(`npm i ${dest}`);
      });
    } catch (e) {
      console.error("Problems:", e);
    }
  }

  static async executeCommand(command) {
    console.log(` ---------> EXECUTING COMMAND: ${command}`);
    const commandExc = await exec(command, {}, (error, stdout, stderr) => {
      console.log(error, stdout, stderr);
    });
  }
  static deleteDirectory(directoryPath) {
    try {
      if (existsSync(directoryPath)) {
        console.log(`Deleting ---------> ${directoryPath}`);
        try {
          rmdirSync(directoryPath, { recursive: true });
        } catch (e) {
          console.error(
            `There was an error deleting ---------> ${directoryPath}`
          );
        }
      }
    } catch (e) {
      console.log(`The directory ${directoryPath} does not exist`);
    }
  }
}
