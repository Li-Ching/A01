package com.example.arhomedesign;

import static com.example.arhomedesign.LoginActivity.TAG;
import static com.unity3d.services.core.properties.ClientProperties.getApplicationContext;

import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;

public class ChangePasswordFragment extends Fragment {
    EditText newPassword, verPassword;
    Button save, cancel;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_change_password, container, false);

        newPassword = view.findViewById(R.id.edtNewPwd);
        verPassword = view.findViewById(R.id.edtVerNewPwd);
        save = view.findViewById(R.id.saveButton);
        cancel = view.findViewById(R.id.cancelButton);

        save.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(!newPassword.equals(verPassword)){
                    Toast.makeText(getContext(), "Invalid Password!", Toast.LENGTH_SHORT).show();
                }
                else{
                    FirebaseUser user = FirebaseAuth.getInstance().getCurrentUser();
                    String password = newPassword.getText().toString();

                    user.updatePassword(password)
                            .addOnCompleteListener(new OnCompleteListener<Void>() {
                                @Override
                                public void onComplete(@NonNull Task<Void> task) {
                                    if (task.isSuccessful()) {
                                        Log.d(TAG, "User password updated.");
                                        FirebaseAuth.getInstance().signOut(); // Firebase 登出
                                    }
                                }
                            });
                }
            }
        });

        cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentManager fragmentManager = getParentFragmentManager();
                fragmentManager.popBackStack();

            }
        });

        return view;
    }
}