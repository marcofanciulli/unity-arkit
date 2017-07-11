import os
import json
import atexit
import subprocess
import time
from BuildLogger import Logger
from thread import start_new_thread


class UnityBuilder():

    def __init__(self, config, logger):
        self.config = config
        self.unity_path = os.path.expanduser(self.config["unity_path"])
        self.unity_log_path = os.path.expanduser(self.config["unity_log_path"])
        if not os.path.exists(self.config["unity_path"]):
            raise Exception("Unity not found on path : " + self.unity_path)
        if not os.path.exists( self.unity_log_path):
            raise Exception("Unity not found on path : " + self.unity_log_path)
        self.logger = logger

    def read_unity_messages(self):
        log_file_unity = open(self.unity_log_path)
        while self.process_unity.poll() is None:
            message = log_file_unity.readline()
            if message:
                self.logger.log(message)

    def wait_for_unity_log_file_updated(self):
        wait_time = 0
        while (not os.path.exists(self.unity_log_path)) and wait_time < 60:
            time.sleep(0.1)
        if wait_time > 60:
            self.logger.log("Unity log file wasn't found for 60 seconds")
            raise Exception("Unity log file wasn't found for 60 seconds")

        if not self.last_log_modification_date:
            return
        wait_time = 0
        while (os.path.getmtime(self.unity_log_path) == self.last_log_modification_date) and wait_time < 60:
            time.sleep(0.1)
        if wait_time > 60:
            raise Exception("Unity log file wasn't modified for 60 seconds")

    def start_unity(self, execute_method):
        project_path = os.path.join(os.environ['WORKSPACE'], self.config["unity_project_dir"])
        command_run_unity = "\"{2}\" -quit -projectPath {0} -batchmode -nographics -executeMethod {1}".format(
            project_path, execute_method,  self.config['unity_path'])
        for argument in self.config["unity_args"]:
            command_run_unity += " -" + argument + " " + self.config["unity_args"][argument]
        if os.path.exists(self.unity_log_path):
            self.last_log_modification_date = os.path.getmtime(self.unity_log_path)
        self.logger.log("Running command : {0}".format(command_run_unity))
        self.process_unity = subprocess.Popen(str(command_run_unity), shell=True, stdout=subprocess.PIPE, close_fds=True)
        return self.process_unity

    def wait_until_unity_finished(self):
        self.wait_for_unity_log_file_updated()
        self.read_unity_messages()
