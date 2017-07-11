import os
import sys
import time
import subprocess
import json
from Modules.BuildLogger import Logger
from Modules.GitExporter import GitExporter

config_path = "config.json"

class BuildDistributor:

    files_to_replace = ["mail.txt"]
    templates_path = os.path.join(os.path.dirname(__file__), "Templates")
    temp_files_path = os.path.join(os.path.dirname(__file__), "Temp")
    appstore_output_path = os.path.join(os.path.dirname(__file__), "..", "JSON.txt")
    git_path = os.path.join(os.path.dirname(__file__), "..\\")

    def __init__(self, config, logger):
        self.config = config
        self.logger = logger
        gitExporter = GitExporter(self.git_path)
        self.dictionary = {
            'T_JOB_NUMBER': "NaN" if not os.environ.has_key("BUILD_NUMBER") else os.environ['BUILD_NUMBER'],
            'T_DATE': time.strftime("%d/%m/%Y") + " " + time.strftime("%H:%M:%S"),
            'T_NAME': self.config["application_name"],
            'T_PAGE': self.get_appstore_link(),
            "T_BRANCH": gitExporter.get_branch(),
            "T_CHANGES": gitExporter.get_last_changes()
        }

    def get_appstore_link(self):
        with open(self.appstore_output_path) as data_file:
            output = json.load(data_file)
            data_file.close()

        link = "itms-services://?action=download-manifest&url=https://appstore.epam.com/downloadplist/{0}/{1}/{2}.plist".\
            format(self.config["appstore_project_id"], str(output["createdBuildId"]), self.config["binary_name"])
        self.logger.log("Build link: " + link)
        return link

    def distribute_build(self):
        self.fill_template_files()
        #self.send_mail()

    def fill_template_files(self):
        for file_to_replace in self.files_to_replace:
            path_th_file_to_replace = os.path.join(self.templates_path, file_to_replace)
            if os.path.exists(path_th_file_to_replace):
                self.replace_file(path_th_file_to_replace)
            else:
                print "Error!  " + path_th_file_to_replace + "  not found!"

    def replace_file(self, path):
        file_to_load = open(path, 'r')
        data_to_save = self.replace_templates_in_data(file_to_load)
        file_to_load.close()

        if not os.path.exists(self.temp_files_path):
            os.makedirs(self.temp_files_path)
        path_to_file_to_save = os.path.join(self.temp_files_path, os.path.basename(path))
        file_to_save = open(path_to_file_to_save, 'w')
        file_to_save.writelines(data_to_save)
        file_to_save.close()

    def replace_templates_in_data(self, data):
        new_data = []
        for line in data:
            print line
            for template in self.dictionary.keys():
                line = line.decode('utf-8').replace(template, self.dictionary[template])
            new_data.append(line)
        data.close()
        return new_data

global config

def load_config():
    global config
    with open(os.path.expanduser(config_path)) as data_file:
        config = json.load(data_file)
        data_file.close()

def main():
    global config
    load_config()
    logger = Logger(os.path.expanduser(config["job_log_path"]))
    distributor = BuildDistributor(config, logger)
    distributor.distribute_build()

if __name__ == "__main__":
    main()