package com.example.arhomedesign;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;

public class FurnitureActivity extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_furniture);

        getSupportActionBar().hide();
    }
}