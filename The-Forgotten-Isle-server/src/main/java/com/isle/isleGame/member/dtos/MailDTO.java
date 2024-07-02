package com.isle.isleGame.member.dtos;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class MailDTO {
    String address;
    String title;
    String message;
}
