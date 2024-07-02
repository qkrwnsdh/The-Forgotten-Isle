package com.isle.isleGame.member.dtos;

import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class JoinDTO {

    @NotBlank
    String username;

    @NotBlank
    String password;

    @NotBlank @Email
    String email;

    @NotBlank
    String ip;
}
