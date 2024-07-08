package com.isle.isleGame.server.dto;

import jakarta.validation.constraints.NotNull;
import lombok.Data;

@Data
public class ServerSaveDTO {
    @NotNull
    int date;
    @NotNull
    int time;
}
