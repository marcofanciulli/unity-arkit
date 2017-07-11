import os
import json
import atexit
from Modules.UnityBuild import UnityBuilder
from Modules.BuildLogger import Logger

class MainBuilder:

    processes = []

    def execute(self, config_path, platform="iOS"):
        self.config_path = config_path
        if not os.environ.has_key("WORKSPACE"):
            os.environ["WORKSPACE"] = os.path.abspath(os.path.join(os.path.dirname(__file__), "..", ".."))
            print "Set up WORKSPACE as : " + os.environ["WORKSPACE"]
        atexit.register(self.cleanup_processes)
        self.load_config()
        self.logger = Logger(os.path.expanduser(self.config["job_log_path"]))
        self.run_unity(platform)

    def load_config(self):
        with open(os.path.expanduser(self.config_path)) as data_file:
            self.config = json.load(data_file)
            data_file.close()

    def cleanup_processes(self):
        for process in self.processes:
            if process is not None:
                process.kill()

    def run_unity(self, platform):
        self.logger.log("Starting unity....")
        unity_builder = UnityBuilder(self.config, self.logger)
        execute_method = ""
        if platform == "iOS":
            execute_method = self.config["unity_iOS_execute_method"]
        elif platform == "Android":
            execute_method = self.config["unity_Android_execute_method"]
        process_unity = unity_builder.start_unity(execute_method)
        self.processes.append(process_unity)
        unity_builder.wait_until_unity_finished()
        self.processes.remove(process_unity)
        self.logger.log("Running Unity was finished with exit code " + str(process_unity.returncode))
        if process_unity.returncode == 1:
            raise Exception("Unity project wasn't build. Unity exit code = 1")

    print "The start of Main Build Script"



