import datetime

class Logger():

    def __init__(self, log_file_path):
        self.log_file_path = log_file_path
        self.clean_log_file()

    def log(self, message):
        output = "[{0}] {1}".format('{:%Y-%m-%d %H:%M:%S}'.format(datetime.datetime.now()) , message)
        print message
        log_file = open(self.log_file_path, 'a')
        log_file.write(output + "\n")
        log_file.close()

    def clean_log_file(self):
        log_file = open(self.log_file_path, 'w')
        log_file.write("")
        log_file.close()