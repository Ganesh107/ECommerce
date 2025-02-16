import React, { useState } from 'react'
import { productModel } from '../utils/productModel'

function AddProduct() {
  const [product, setProduct] = useState(productModel)
  const handleImageUpload = (e) => {
    const files = e.target.files;
    // Convert files as byte stream
    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      const reader = new FileReader();
      reader.onloadend = () => {
        // Convert the image to a Base64 string (byte stream)
        const base64String = reader.result;
        setProduct((prevProduct) => ({
          ...prevProduct,
          images: [...prevProduct.images, base64String]
        }));
      };

      // Read the file as a Data URL (Base64)
      reader.readAsDataURL(file);
    }
  };

  return (
    <div className='flex flex-col gap-3 p-3'> 
      <div className='flex gap-x-3'>
        <p>Product Name</p>
          <input value={product.productName} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Product Name' 
          onChange={e => setProduct({...product, productName: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Brand</p>
        <input value={product.brand} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Brand' 
          onChange={e => setProduct({...product, brand: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Model Number</p>
        <input value={product.modelNumber} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Model Number' 
          onChange={e => setProduct({...product, modelNumber: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Model Name</p>
        <input value={product.modelName} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Model Name' 
          onChange={e => setProduct({...product, modelName: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Current price</p>
        <input value={product.currentPrice} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Current price' 
          onChange={e => setProduct({...product, currentPrice: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Original price</p>
        <input value={product.originalPrice} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Original price' 
          onChange={e => setProduct({...product, originalPrice: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Memory</p>
        <input value={product.memory} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Memory' 
          onChange={e => setProduct({...product, memory: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Category</p>
        <input value={product.category} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Category' 
          onChange={e => setProduct({...product, category: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>sub category</p>
        <input value={product.subCategory} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='sub category' 
          onChange={e => setProduct({...product, subCategory: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Color</p>
        <input value={product.color} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Color' 
          onChange={e => setProduct({...product, color: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Count</p>
        <input value={product.count} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Count' 
          onChange={e => setProduct({...product, count: e.target.value})}/>
      </div>
      <div className='flex gap-x-3'>
        <p>Size</p>
        <input value={product.size} 
          className='border border-black rounded-md outline-none'
          type='text' placeholder='Size' 
          onChange={e => setProduct({...product, size: e.target.value})}/>
      </div>
      <div className>
        <p>Upload Images</p>
        <input
          type='file'
          accept='image/*' 
          multiple
          onChange={handleImageUpload}
          className='w-full p-2 border rounded'
        />
      </div>
      <div className='flex flex-wrap gap-2'>
        {product.images.map((image, index) => (
          <img
            key={index}
            src={image}
            alt={`Uploaded ${index}`}
            className='w-24 h-24 object-cover border rounded'
          />
        ))}
      </div>
    </div>
  )
}

export default AddProduct