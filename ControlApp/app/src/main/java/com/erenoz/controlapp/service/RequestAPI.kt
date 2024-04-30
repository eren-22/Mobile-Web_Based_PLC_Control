package com.erenoz.controlapp.service

import com.erenoz.controlapp.model.RequestModel
import retrofit2.Call
import retrofit2.http.GET

interface RequestAPI {

    @GET("api/request/getValue")
    fun getData() : Call<List<RequestModel>>
}