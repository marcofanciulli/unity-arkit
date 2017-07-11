import os
import subprocess
import re


class GitExporter:

    def __init__(self, git_local_path):
        self.git_path = git_local_path

    def get_last_changes(self):
        try:
            last_revision = os.environ["GIT_PREVIOUS_SUCCESSFUL_COMMIT"]
        except Exception:
            print "WARNING! Can't find previous successful commit"
            last_revision = "HEAD"

        command = "cd " + self.git_path + \
                  " & git rev-list --first-parent --pretty=format:%an%n%s%n " + last_revision + "..HEAD"

        try:
            last_commits = subprocess.check_output(command, shell=True)
        except Exception:
            last_commits = "Error! Didn't get last commits."

        reCommit = re.compile(r'^\bcommit\b.')
        pretty_last_commits = ""
        for line in last_commits.split('\n'):
            if ((re.search(reCommit, line) != None)):
                pretty_last_commits += ""
            else:
                pretty_last_commits += "\n" + line
        return pretty_last_commits

    def get_branch(self):
        if os.environ.has_key("GIT_BRANCH"):
            return os.environ["GIT_BRANCH"]
        command = "cd " + self.git_path + \
                  " & git rev-parse --abbrev-ref HEAD"
        try:
            branch = subprocess.check_output(command, shell=True)
        except Exception:
            print Exception.message
            branch = "Can't get branch"
        return branch

