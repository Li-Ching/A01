package com.example.arhomedesign;

import androidx.appcompat.app.AppCompatActivity;

import android.animation.LayoutTransition;
import android.content.Intent;
import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;


import com.google.android.gms.auth.api.signin.GoogleSignIn;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInClient;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.SignInButton;
import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.tasks.Task;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;


public class LoginActivity extends AppCompatActivity {
    EditText Email, Password;
    TextView signUp, fgtPwd;
    Button loginButton;
    LinearLayout back;

    public static final String TAG = LoginActivity.class.getSimpleName()+"My";

    private GoogleSignInClient mGoogleSignInClient;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        getSupportActionBar().hide();

        Email = findViewById(R.id.edtEmail);
        Password = findViewById(R.id.edtPassword);
        loginButton = findViewById(R.id.loginButton);
        signUp = findViewById(R.id.signupButton);
        fgtPwd = findViewById(R.id.tvFgtPwd);
        back = findViewById(R.id.btnBack);

        GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
                .requestIdToken("226596953948-msq60v6f1472i2308jkg9hn8bujc149u.apps.googleusercontent.com")
                .requestEmail()
                .build();

        mGoogleSignInClient = GoogleSignIn.getClient(this, gso);

        SignInButton btSighIn = findViewById(R.id.button_SignIn);
        btSighIn.setOnClickListener(v->{
            startActivityForResult(mGoogleSignInClient.getSignInIntent(),200);
        });

        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String email = Email.getText().toString();
                String password = Password.getText().toString();
                //createAuthToken(email, password);

                if(email.length()==0 || password.length()==0){
                    Log.d("API Response", "長度不對");

                    Toast.makeText(getApplicationContext(), "Invalid Username or Password!", Toast.LENGTH_SHORT).show();
                }
                else {
                    Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);
                    LoginData modal = new LoginData(email, password);
                    Call<LoginData> call = methods.Login(modal);
                    call.enqueue(new Callback<LoginData>() {
                        @Override
                        public void onResponse(Call<LoginData> call, Response<LoginData> response) {
                            // on below line we are setting empty text
                            // to our both edit text.
                            Email.setText("");
                            Password.setText("");
                            if (response.isSuccessful()) {
                                Log.d("API Response", "呼叫成功");

                                // 登入成功
                                int statusCode = response.code(); // 取得回應狀態碼
                                if (statusCode == 200) {
                                    Log.d("API Response", "ok");

                                    // 登入成功
                                    Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                                    startActivity(intent);
                                    finish();
                                } else {
                                    Log.d("API Response", "帳號密碼錯誤");

                                    // 登入失敗
                                    // 可以顯示 "帳號密碼錯誤" 的錯誤訊息或執行其他處理邏輯
                                    Toast.makeText(getApplicationContext(), "帳號密碼錯誤", Toast.LENGTH_SHORT).show();
                                }
                            } else {
                                Log.d("API Response", "呼叫失敗");

                                // API 呼叫失敗
                                // 可以顯示錯誤訊息或執行其他處理邏輯
                                Toast.makeText(getApplicationContext(), "API 呼叫失敗", Toast.LENGTH_SHORT).show();
                            }
                        }

                        @Override
                        public void onFailure(Call<LoginData> call, Throwable t) {
                            // setting text to our text view when
                            // we get error response from API.
                            Log.e("API Response", "Error: " + t.getMessage());
                        }
                    });
                }
            }
        });

        signUp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(LoginActivity.this, SignupActivity.class);
                startActivity(intent);
            }
        });

        fgtPwd.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(LoginActivity.this, ForgotPwdActivity.class);
                startActivity(intent);
            }
        });

        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(LoginActivity.this, Intro.class);
                startActivity(intent);
            }
        });
    }
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == 200) {
            Task<GoogleSignInAccount> task = GoogleSignIn.getSignedInAccountFromIntent(data);
            try {
                GoogleSignInAccount account = task.getResult(ApiException.class);
                String result = "登入成功\nEmail："+account.getEmail()+"\nGoogle名稱："
                        +account.getDisplayName();
                String idToken = account.getIdToken();
                Log.d(TAG, "Token: "+idToken);
                Log.d(TAG, "Result: "+result);
                //TextView tvResult = findViewById(R.id.textView_Result);
                //tvResult.setText(result);
            } catch (ApiException e) {
                Log.w(TAG, "Google sign in failed", e);
            }
        }
    }
}