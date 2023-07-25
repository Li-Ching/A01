package com.example.arhomedesign;

import android.app.ActivityOptions;
import android.content.Intent;
import android.os.Bundle;
import androidx.appcompat.app.AppCompatActivity;
import android.os.Handler;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;

public class Intro extends AppCompatActivity {
    Button login, guest;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_intro);

        getSupportActionBar().hide();

        login = findViewById(R.id.btnLogin);
        guest = findViewById(R.id.btnGuest);

        login.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(Intro.this, LoginActivity.class);
                startActivity(intent);
                finish();
            }
        });

        guest.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(Intro.this, MainActivityNoLogin.class);
                startActivity(intent);
                finish();
            }
        });

    }
}