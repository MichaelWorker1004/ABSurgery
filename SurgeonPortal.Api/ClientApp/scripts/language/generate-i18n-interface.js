import {
  copyFileSync,
  existsSync,
  lstatSync,
  mkdirSync,
  readdirSync,
  readFileSync,
  writeFileSync,
} from "fs";
import process from "process";
import * as path from "path";

export class TranslationFilesParser {
  static interfaceDefs;
  static classAsString = "";
  static depthCnt = 0;

  /**
   * Recursively copies a directory from one location
   * to another
   * @param {string} source
   * @param {string} target
   */
  static copyFolderRecursiveSync(source, target) {
    console.log("Copying ", source, "to", target);
    let files = [];
    const targetFolder = path.join(target, path.basename(source));
    if (!existsSync(targetFolder)) {
      mkdirSync(targetFolder);
    }

    if (lstatSync(source).isDirectory()) {
      files = readdirSync(source);
      files.forEach(function (file) {
        let curSource = path.join(source, file);
        if (lstatSync(curSource).isDirectory()) {
          TranslationFilesParser.copyFolderRecursiveSync(
            curSource,
            targetFolder
          );
        } else {
          copyFileSync(curSource, `${targetFolder}/${file}`);
        }
      });
    }
  }

  static generateLangTypeScript(
    sourceDirectory,
    sourceFile,
    targetDirectories,
    localizationDirectories
  ) {
    const filePath = sourceDirectory + sourceFile;
    if (existsSync(filePath)) {
      try {
        const langFile = readFileSync(filePath, {
          encoding: "utf8",
          flag: "r",
        });
        let output = TranslationFilesParser.generateClass(
          "LanguageLabelsNGX",
          langFile
        );
        // Autogen the interfaces and class, then write to a file.
        targetDirectories.forEach((targetDirectory) => {
          console.log(`Generating Typescript files to ${targetDirectory}`);
          writeFileSync(`${targetDirectory}language-labels.ts`, output);
        });
        // copy localization files to each project
        localizationDirectories.forEach((localizationDirectory) => {
          TranslationFilesParser.copyFolderRecursiveSync(
            sourceDirectory,
            localizationDirectory
          );
        });
      } catch (e) {
        console.log(
          "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"
        );
        console.error("Error parsing JSON for translation files", e);
        throw e;
      }
    } else {
      console.error(
        `The path and the file ${filePath} were not found. Please check them to make sure they exist in the directory structure`
      );
    }
  }

  static generateClass(className, jsonObj) {
    const sourceObj = JSON.parse(jsonObj);
    TranslationFilesParser.interfaceDefs = [];
    TranslationFilesParser.walkTree(sourceObj, [], "I" + className);

    let output = TranslationFilesParser.generateInterfaces();
    output += TranslationFilesParser.generateClassDef(
      className,
      sourceObj,
      "I" + className
    );
    output += TranslationFilesParser.getLanguageHelperClass();

    return output;
  }

  static generateInterfaces() {
    let output = "";

    output += TranslationFilesParser.getFileHeaders();

    TranslationFilesParser.interfaceDefs.forEach((intf) => {
      output += `export interface ${intf.name} {\n`;
      intf.props.forEach((prop) => {
        output += `  ${prop.name}: ${prop.type};\n`;
      });
      output += `}\n`;
    });

    return output;
  }

  static generateClassDef(className, sourceObj, interfaceName) {
    let output = "\n";
    output += `export class ${className} implements ${interfaceName} {\n`;
    output += TranslationFilesParser.classAsString;
    return output;
  }

  static walkTree(obj, propArray, interfaceName) {
    let interfaceObj = { name: interfaceName, props: [] };
    Object.keys(obj).forEach((key) => {
      if (typeof obj[key] === "object") {
        TranslationFilesParser.depthCnt++;
        const nxtarray = propArray.concat([key]);
        nxtarray.forEach((item) => {
          TranslationFilesParser.classAsString += `  `;
        });
        if (TranslationFilesParser.depthCnt === 1) {
          TranslationFilesParser.classAsString += `${key} = {\n`;
        } else {
          TranslationFilesParser.classAsString += `${key}: {\n`;
        }
        const interfaceName = "I" + nxtarray.toString().replace(/,/g, "_");
        interfaceObj.props.push({ name: key, type: interfaceName });
        TranslationFilesParser.walkTree(obj[key], nxtarray, interfaceName);
      } else {
        const endarray = propArray.concat([key]);
        const propsString = endarray.toString().replace(/,/g, ".");
        obj[key] = propsString;
        endarray.forEach((item) => {
          TranslationFilesParser.classAsString += `  `;
        });
        let lineEndings;
        let propertyAssign;
        if (TranslationFilesParser.depthCnt === 0) {
          propertyAssign = ` =`;
          lineEndings = `;\n`;
        } else {
          propertyAssign = `:`;
          lineEndings = `,\n`;
        }
        TranslationFilesParser.classAsString += `${key}${propertyAssign} "${propsString}"`;
        TranslationFilesParser.classAsString += lineEndings;
        interfaceObj.props.push({ name: key, type: "string" });
      }
    });
    for (let i = 0; i < TranslationFilesParser.depthCnt; i++) {
      TranslationFilesParser.classAsString += `  `;
    }
    TranslationFilesParser.classAsString += `}`;
    switch (TranslationFilesParser.depthCnt) {
      case 0:
        TranslationFilesParser.classAsString += `\n`;
        break;
      case 1:
        TranslationFilesParser.classAsString += `;\n`;
        break;
      default:
        TranslationFilesParser.classAsString += `,\n`;
        break;
    }
    TranslationFilesParser.depthCnt--;
    TranslationFilesParser.interfaceDefs.push(interfaceObj);
  }

  static getFileHeaders() {
    let output = "";

    // Add warning
    output += `/****************************************************************************************\n`;
    output += `This file is auto generated. Any changes to this file will be overwritten\n`;
    output += `****************************************************************************************/\n`;

    output += `import {TranslateService} from "@ngx-translate/core";\n`;
    output += `import {Injectable} from "@angular/core";\n\n`;

    output += `export const supportedLanguages: string[] = ["en-US", "fr-CA", "en-CA"];\n\n`;

    return output;
  }

  static getLanguageHelperClass() {
    let output = "\n\n";
    output += `
@Injectable()
export class LanguageHelper {

  constructor(
    private translateService: TranslateService
  ) {
  }

  setupLanguage(lang?: string | undefined): void {
    this.translateService.addLangs(supportedLanguages);
    this.translateService.setDefaultLang(supportedLanguages[0]);

    let browserLang = this.translateService.getBrowserLang();
    browserLang = browserLang ? browserLang : supportedLanguages[0];
    lang = lang? lang : browserLang;

    this.translateService.use( supportedLanguages.includes(lang) ? lang : supportedLanguages[0]);
  }
}

\n\n`;
    return output;
  }
}

const sourceDirectory = process.argv.slice(2)[0];
const sourceFile = process.argv.slice(2)[1];
const targetDirectories = [`${process.argv.slice(2)[2]}/src/app/`];
const localizationDirectories = [`${process.argv.slice(2)[2]}/src/assets/`];

TranslationFilesParser.generateLangTypeScript(
  sourceDirectory,
  sourceFile,
  targetDirectories,
  localizationDirectories
);
