import os
import subprocess


class XcodeBuild:

    def change_bundle(self, bundle_id):
        command_change_bundle = "/usr/libexec/PlistBuddy {0} -c Set :CFBundleIdentifier {1}".format(self.info_plist_path, bundle_id)
        self.process_bundle = subprocess.Popen(command_change_bundle, shell=True, stdout=subprocess.PIPE, close_fds=True)

    def build_project(self):
        command_build_project = "/usr/bin/xcodebuild -target Unity-iPhone -project {0} -configuration {1} clean build CONFIGURATION_BUILD_DIR={2} CODE_SIGN_RESOURCE_RULES_PATH=$(SDKROOT)/ResourceRules.plist{3} CODE_SIGN_IDENTITY=\"{4}\" BUNDLE_IDENTITY={5}".format(
            self.config["xcode_project_path"], self.config["xcode_build_coniguration"], self.config["xcode_binary_path"], "",
            self.config["xcode_signing_identity"], self.config["xcode_bundle_id"])
        self.logger.log(command_build_project)
        xcode_process = subprocess.Popen(command_build_project, shell=True, stdout=subprocess.PIPE, close_fds=True)
        while True:
            output = xcode_process.stdout.readline()
            if output == '' and xcode_process.poll() is not None:
                break
            if output:
                if output.find("builtin-validationUtility") > -1:
                    self.binary_app_path = output.split("builtin-validationUtility ")[1].replace("\n", "")
                self.logger.log(output.strip())


    def pack_to_ipa(self):
        command_pack_ipa = "xcrun -sdk iphoneos PackageApplication -v \"{0}\" -o \"{1}\" -embed \"{2}\"".format(
            self.binary_app_path,
            os.path.join(os.path.dirname(self.binary_app_path), str(self.config["application_name"]).replace(" ", "_") + ".ipa"),
            self.config["xcode_provision_profile_path"])
        self.logger.log(command_pack_ipa)
        xrun_procenss = subprocess.Popen(command_pack_ipa, shell=True, stdout=subprocess.PIPE, close_fds=True)
        while True:
            output = xrun_procenss.stdout.readline()
            if output == '' and xrun_procenss.poll() is not None:
                break
            if output:
                self.logger.log(output.strip())


    def __init__(self, config, logger):
        self.config = config
        self.logger = logger