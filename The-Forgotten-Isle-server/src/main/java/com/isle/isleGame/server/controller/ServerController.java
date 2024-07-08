package com.isle.isleGame.server.controller;

import com.isle.isleGame.server.dto.ServerSaveDTO;
import com.isle.isleGame.server.service.ServerService;
import com.isle.isleGame.server.dto.ServerAddDTO;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
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
    public ResponseEntity<Object> addServer(@Valid @RequestBody ServerAddDTO serverAddDTO) {
        String username = SecurityContextHolder.getContext().getAuthentication().getName();
        return serverService.addServer(username, serverAddDTO);
    }

    @GetMapping
    public ResponseEntity<Object> loadServer(@RequestParam @NotNull int server_ID) {
        return serverService.loadServer(server_ID);
    }

    @PostMapping("/save")
    public ResponseEntity<Object> saveServer(@RequestParam @NotNull int server_ID, @Valid @RequestBody ServerSaveDTO dto) {
        return serverService.saveServer(server_ID, dto);
    }

    @DeleteMapping
    public ResponseEntity<Object> deleteServer(@RequestParam @NotNull int server_ID) {
        return serverService.deleteServer(server_ID);
    }
}
