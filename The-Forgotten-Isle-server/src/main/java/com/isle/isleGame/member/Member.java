package com.isle.isleGame.member;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import lombok.Data;

@Entity
@Data
public class Member {

    @Id @Column(name = "member_id")
    private String id;

    @Column(nullable = false)
    private String password;
}
