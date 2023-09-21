import fs, { readFileSync, existsSync, mkdirSync } from "fs";
import * as process from "process";
import path from "path";
import * as XLSX from "xlsx";
import { Readable } from "stream";
import { fileURLToPath } from "url";

// A JS Module way to do the node.js __dirname
const __dirname = path.dirname(fileURLToPath(import.meta.url));

XLSX.set_fs(fs); // DO NOT FORGET THIS LINE! (required for use with raw data)
XLSX.stream.set_readable(Readable); // DO NOT FORGET THIS LINE! (required for use with raw data)

/**
 * A class to read from an excel file and write to a set of locale files
 */
export class ReadFromLocales {
  static locales;
  static translationMap;
  static rows;
  static spreadsheetObject;
  static outputDirectory;

  /**
   * Read from an excel file and write to a set of locale files
   * @param localePaths
   * @param outputDirectory
   * @returns {Promise<void>}
   */
  static async writeFromLocalesToExcel(localePaths, outputDirectory) {
    debugger;

    // Set the static properties
    ReadFromLocales.outputDirectory = outputDirectory;
    ReadFromLocales.translationMap = new Map();
    ReadFromLocales.rows = 0;
    ReadFromLocales.locales = [];
    ReadFromLocales.spreadsheetObject = [];

    // Load the translation files
    const files = await ReadFromLocales.loadTranslationFiles(localePaths);
    files.forEach((file) => {
      ReadFromLocales.locales.push(file.meta.locale);
      ReadFromLocales.walkTree(file, [], file.meta.locale);
    });
    ReadFromLocales.mapToSpreadsheet();
    ReadFromLocales.writeXLSX();
  }

  /**
   * Load the translation files
   * @param {string[]} filePaths - The paths to the translation files
   * @returns {Promise<*[]>}
   */
  static async loadTranslationFiles(filePaths) {
    const files = [];
    filePaths.forEach((filePath) => {
      if (existsSync(filePath)) {
        const file = readFileSync(filePath, "utf8");
        files.push(JSON.parse(file));
      }
    });
    return files;
  }

  /**
   * This is a recursive method that walks the tree of the translation files
   * @param obj
   * @param propArray
   * @param locale
   */
  static walkTree(obj, propArray, locale) {
    // obj is the object to be parsed
    // propArray is an array of properties that have been traversed
    // The traversal is done recursively and when the end of the object is reached
    // The final property is placed into the map value based on the key
    Object.keys(obj).forEach((key) => {
      if (typeof obj[key] === "object") {
        const nxtarray = propArray.concat([key]);
        ReadFromLocales.walkTree(obj[key], nxtarray, locale);
      } else {
        const endarray = propArray.concat([key]);
        const propsString = endarray.toString().replace(/,/g, ".");
        const keyMap = ReadFromLocales.translationMap.get(propsString);
        if (keyMap) {
          keyMap.set(locale, obj[key]);
        } else {
          const localeMap = new Map();
          localeMap.set(locale, obj[key]);
          ReadFromLocales.translationMap.set(propsString, localeMap);
          ReadFromLocales.rows++;
        }
      }
    });
  }

  /**
   * Map the translation map to a spreadsheet object
   */
  static mapToSpreadsheet() {
    const rows = [];
    ReadFromLocales.translationMap.forEach((value, key) => {
      const row = {};
      row["key"] = key;
      ReadFromLocales.locales.forEach((locale) => {
        if (value.get(locale)) {
          row[locale] = value.get(locale);
        }
      });
      rows.push(row);
    });
    ReadFromLocales.spreadsheetObject = rows;
  }

  /**
   * Write the spreadsheet object to an excel file
   */
  static writeXLSX() {
    const rows = ReadFromLocales.spreadsheetObject;
    const workbook = XLSX.utils.book_new();
    const worksheet = XLSX.utils.json_to_sheet(rows);
    const headers = ["key", ...ReadFromLocales.locales];

    // Set a min width, start with one for the key column
    // Set text to wrap in all cells
    const cols = [{ width: 30 }];
    headers.forEach((value, index) => {
      cols.push({ width: 30 });
    });

    // TO Sort on the entire data set, the first key in the auto-sort is A1, the last is the
    // Corresponding letter to the length of columns,
    // and the very last cell of data, otherwise you will get an error
    worksheet["!autofilter"] = {
      ref: `A1:${ReadFromLocales.getColumnName(
        ReadFromLocales.locales.length
      )}${rows.length}`,
    };

    worksheet["!cols"] = cols;

    XLSX.utils.book_append_sheet(
      workbook,
      worksheet,
      "Translation Keys for Export"
    );

    // Check to make sure directory exists
    if (!existsSync(ReadFromLocales.outputDirectory)) {
      mkdirSync(ReadFromLocales.outputDirectory, { recursive: true });
    }
    XLSX.writeFile(
      workbook,
      `${ReadFromLocales.outputDirectory}/translations.xlsx`,
      {
        compression: true,
      }
    );
  }

  /**
   * Get the column name based on the location
   * @param location
   * @returns {string}
   */
  static getColumnName(location) {
    const letters = [
      "A",
      "B",
      "C",
      "D",
      "E",
      "F",
      "G",
      "H",
      "I",
      "J",
      "K",
      "L",
      "M",
      "N",
      "O",
      "P",
      "Q",
      "R",
      "S",
      "T",
      "U",
      "V",
      "W",
      "X",
      "Y",
      "Z",
    ];
    return letters[location];
  }
}

const paths = process.argv.slice(3);
const outputDirectory = process.argv[2];

console.info(
  "Paths: outputDirectory",
  outputDirectory,
  "paths",
  paths.toString().split(",")
);

ReadFromLocales.writeFromLocalesToExcel(
  paths.toString().split(","),
  outputDirectory
)
  .then((result) => {
    console.info("Translations Complete");
  })
  .catch((error) => {
    console.error(error);
  });
