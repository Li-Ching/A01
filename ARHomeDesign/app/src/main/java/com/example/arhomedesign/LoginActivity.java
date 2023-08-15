package com.example.arhomedesign;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
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
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;

public class LoginActivity extends AppCompatActivity {
    EditText Email, Password;
    private SignInButton btnSignIn; // 將 SignInButton 改為 btnSignIn
    private Button btnLogin; // 新增對應的 loginButton 按鈕
    private Button btnSignup; // 新增對應的 signupButton 按鈕
    private TextView forgotPassword;

    public static final String TAG = LoginActivity.class.getSimpleName() + "My";

    private GoogleSignInClient mGoogleSignInClient;
    private FirebaseAuth mAuth;
    private FirebaseAuth.AuthStateListener mAuthListener;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        getSupportActionBar().hide();
        findviewById();
        mAuth = FirebaseAuth.getInstance();

        mAuthListener = new FirebaseAuth.AuthStateListener() {
            @Override
            public void onAuthStateChanged(@NonNull FirebaseAuth firebaseAuth) {
                FirebaseUser user = firebaseAuth.getCurrentUser();
                if (user != null) {
                    // User is signed in
                    Log.d("TAG", user.getUid());
                    // 跳轉至 MainActivity
                    startActivity(new Intent(LoginActivity.this, MainActivity.class));
                    finish(); // 結束 LoginActivity，避免返回到登入頁面
                } else {
                    // User is signed out
                    Log.d("Tag", "user ==null");
                }
                // ...
            }
        };

        GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
                .requestIdToken("226596953948-msq60v6f1472i2308jkg9hn8bujc149u.apps.googleusercontent.com")
                .requestEmail()
                .build();

        mGoogleSignInClient = GoogleSignIn.getClient(this, gso);

        // 新增對應的 loginButton 按鈕點擊事件監聽器
        btnLogin.setOnClickListener(btnListener);
        // 新增對應的 signupButton 按鈕點擊事件監聽器
        btnSignup.setOnClickListener(btnListener);

        forgotPassword.setOnClickListener(btnListener);

        // 修改 SignInButton 的點擊事件監聽器
        btnSignIn.setOnClickListener(v -> {
            startActivityForResult(mGoogleSignInClient.getSignInIntent(), 200);
        });
    }



    private View.OnClickListener btnListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            if (v.getId() == R.id.loginButton) {
                String email = Email.getText().toString();
                String password = Password.getText().toString();
                mAuth.signInWithEmailAndPassword(email, password)
                        .addOnCompleteListener(new OnCompleteListener<AuthResult>() {
                            @Override
                            public void onComplete(@NonNull Task<AuthResult> task) {
                                if (task.isSuccessful()) {
                                    Log.d("TAG", "登入成功");
                                } else {
                                    Log.d("TAG", "登入失敗");
                                }
                            }
                        });
            }
            if (v.getId() == R.id.signupButton) {
                startActivity(new Intent(LoginActivity.this, SignupActivity.class));
            }
            if (v.getId() == R.id.tvFgtPwd) {
                startActivity(new Intent(LoginActivity.this, ForgotPwdActivity.class));
            }
        }
    };

    private void findviewById() {
        btnSignIn = findViewById(R.id.button_SignIn);
        btnLogin = findViewById(R.id.loginButton); // 將 loginButton 對應到對應的 id
        btnSignup = findViewById(R.id.signupButton); // 將 signupButton 對應到對應的 id
        forgotPassword = findViewById(R.id.tvFgtPwd);

        Email = findViewById(R.id.edtEmail);
        Password = findViewById(R.id.edtPassword);
    }

    public void onStart() {
        super.onStart();
        mAuth.addAuthStateListener(mAuthListener);
    }

    public void onStop() {
        super.onStop();
        if (mAuthListener != null) {
            mAuth.removeAuthStateListener(mAuthListener);
        }
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == 200) {
            Task<GoogleSignInAccount> task = GoogleSignIn.getSignedInAccountFromIntent(data);
            try {
                GoogleSignInAccount account = task.getResult(ApiException.class);
                String result = "登入成功\nEmail：" + account.getEmail() + "\nGoogle名稱：" + account.getDisplayName();
                String idToken = account.getIdToken();
                Log.d(TAG, "Token: " + idToken);
                Log.d(TAG, "Result: " + result);
            } catch (ApiException e) {
                Log.w(TAG, "Google sign in failed", e);
            }
        }
    }
}
