package com.github.tornaia.jshelloverlayiconapp;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.nio.file.Paths;

@RequestMapping("/file-status")
@RestController
public class FileStatusController {

    private static final Logger LOG = LoggerFactory.getLogger(FileStatusController.class);

    @Autowired
    private FileStatusService fileStatusService;

    @RequestMapping(method = RequestMethod.GET)
    public FileStatus getFileStatus(@RequestParam(value = "absolutePath") String absolutePath) {
        FileStatus fileStatus = fileStatusService.getFileStatus(Paths.get(absolutePath));
        LOG.info("FileStatusController. absolutePath: {}, fileStatus: {}", absolutePath, fileStatus);
        return fileStatus;
    }
}
