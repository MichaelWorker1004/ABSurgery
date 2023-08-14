import {
  constants,
  copyFile,
  existsSync,
  readFileSync,
  writeFileSync,
  rmdirSync,
} from "fs";
import { exec } from "child_process";

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
    return await new Promise((resolve, reject) => {
      exec(command, {}, (error, stdout, stderr) => {
        console.log(error, stdout, stderr);
        resolve(true);
      });
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

  static async updatePackageJsonVersion(packageJsonPath, version) {
    const packageJson = JSON.parse(
      readFileSync(packageJsonPath, {
        encoding: "utf8",
      })
    );
    packageJson.version = version;
    writeFileSync(packageJsonPath, JSON.stringify(packageJson, null, 2), {
      encoding: "utf8",
    });
    return packageJson.version;
  }

  static async setBuildVersion(jsonPath, version) {
    const configJson = JSON.parse(
      readFileSync(jsonPath, {
        encoding: "utf8",
      })
    );
    configJson.buildId = version;
    writeFileSync(jsonPath, JSON.stringify(configJson, null, 2), {
      encoding: "utf8",
    });
    return configJson.buildId;
  }
}
