package com.isle.isleGame.member.dtos;

import jakarta.validation.constraints.NotBlank;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class SetPasswordDTO {
    @NotBlank
    String password;
    @NotBlank
    String new_password;
}
