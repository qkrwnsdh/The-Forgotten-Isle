package com.isle.isleGame.server.controller;

import com.isle.isleGame.server.service.ServerService;
import com.isle.isleGame.server.dto.ServerAddDTO;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/server")
public class ServerController {

    private final ServerService serverService;

    public ServerController(ServerService serverService) {
        this.serverService = serverService;
    }

    @PostMapping
    public ResponseEntity<Object> addServer(@RequestBody ServerAddDTO serverAddDTO) {
        String username = SecurityContextHolder.getContext().getAuthentication().getName();
        return serverService.addServer(username, serverAddDTO);
    }

    @GetMapping
    public ResponseEntity<Object> loadServer(@RequestParam int server_ID) {
        return serverService.loadServer(server_ID);
    }


}
