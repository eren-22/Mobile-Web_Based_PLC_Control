package com.erenoz.controlapp.view

import android.content.res.ColorStateList
import android.graphics.Color
import android.os.Bundle
import android.view.View
import android.widget.ImageView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.bumptech.glide.Glide
import com.erenoz.controlapp.R
import com.erenoz.controlapp.databinding.ActivityMainBinding
import com.erenoz.controlapp.model.RequestModel
import com.erenoz.controlapp.service.RequestAPI
import okhttp3.OkHttpClient
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.security.KeyStore
import java.security.cert.CertificateFactory
import javax.net.ssl.SSLContext
import javax.net.ssl.TrustManager
import javax.net.ssl.TrustManagerFactory
import javax.net.ssl.X509TrustManager

class MainActivity : AppCompatActivity() {

    private val BASE_URL = "http://192.168.0.74:8090/"

    private var requestModels: ArrayList<RequestModel>? = null

    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        val view = binding.root
        setContentView(view)

        showPhoto()
    }

    fun buttonClicked(view: View){
        loadData()
    }

    private fun loadData() {
        val retrofit = Retrofit.Builder()
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .build()

        val service = retrofit.create(RequestAPI::class.java)
        val call = service.getData()

        call.enqueue(object : Callback<List<RequestModel>> {
            override fun onResponse(
                call: Call<List<RequestModel>>,
                response: Response<List<RequestModel>>
            ) {
                if (response.isSuccessful) {
                    response.body()?.let {
                        requestModels = ArrayList(it)

                        binding.toggleButton.setOnCheckedChangeListener { buttonView, isChecked ->
                            if (isChecked) {
                                // Toggle buton açık
                                showGIF()
                                binding.toggleButton.backgroundTintList = ColorStateList.valueOf(Color.GREEN)
                                Toast.makeText(this@MainActivity,"The ventilator has been activated.",Toast.LENGTH_SHORT).show()

                            } else {
                                // Toggle buton kapalı
                                showPhoto()
                                binding.toggleButton.backgroundTintList = ColorStateList.valueOf(Color.RED)
                                Toast.makeText(this@MainActivity,"The ventilator was turned off.",Toast.LENGTH_SHORT).show()

                            }
                        }

                        for (requestModel: RequestModel in requestModels!!) {
                            println(requestModel.id)
                            println(requestModel.value)
                            println(requestModel.station)
                        }
                    }
                } else {
                    Toast.makeText(this@MainActivity,"An error occurred in the connection.",Toast.LENGTH_LONG).show()
                }
            }

            override fun onFailure(call: Call<List<RequestModel>>, t: Throwable) {
                println("Request failed: ${t.message}")
            }
        })
    }

    fun showGIF(){
        val imageView: ImageView = findViewById(R.id.imageView)
        Glide.with(this).load(R.drawable.ventilator).into(imageView)
    }

    fun showPhoto(){
        val imageView: ImageView = findViewById(R.id.imageView)
        imageView.setImageResource(R.drawable.ventilator)
    }
}