package com.isle.isleGame.member.dtos;

import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class JoinDTO {

    @NotNull
    String username;

    @NotNull
    String password;

    @NotNull @Email
    String email;

    String ip;
}
